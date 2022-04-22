public class InverseScara
    {
        public float L1 { get; set; }
        public float L2 { get; set; }
        public float L3 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="l1"></param>
        /// <param name="l2"></param>
        /// <param name="l3"></param>
        public InverseScara(float l1, float l2, float l3)
        {
            L1 = l1;
            L2 = l2;
            L3 = l3;
        }
        
        public InverseScara()
        {
            L1 = 0.139f;
            L2 = 0.300f;
            L3 = 0.300f;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="positionXYZtheta"></param>
        /// <returns></returns>
        public Tuple<Vector4, Vector4> Solve(Vector4 positionXYZtheta){
        var px=positionXYZtheta.x;
        var py=positionXYZtheta.y;
        var pz=positionXYZtheta.z;
        var rz = positionXYZtheta.w;
        
        var a1=L2;
        var a2=L3;
        
        var up=px*px+py*py-a1*a1-a2*a2;
        var down=2*a1*a2;
        var q21=Mathf.Acos(up/down);
        var q22=-q21;
    
        var p1=a2*Mathf.Sin(q21)*px+(a1+a2*Mathf.Cos(q21))*py;
        var p2=(a1+a2*Mathf.Cos(q21))*px-a2*Mathf.Sin(q21)*py;
        var q12=Mathf.Atan2(p1,p2);
    
        p1=a2*Mathf.Sin(q22)*px+(a1+a2*Mathf.Cos(q22))*py;
        p2=(a1+a2*Mathf.Cos(q22))*px-a2*Mathf.Sin(q22)*py;
        var q11=Mathf.Atan2(p1,p2);

        var t3=pz-L1;

        var q41=rz-q11-q21;
        q41 = Mathf.Atan2(Mathf.Sin(q41), Mathf.Cos(q41));
        var q42=rz-q12-q22;
        q42 = Mathf.Atan2(Mathf.Sin(q42), Mathf.Cos(q42));
        
        var s1=new Vector4(q11*Mathf.Rad2Deg,q21*Mathf.Rad2Deg,t3,q41*Mathf.Rad2Deg);
        var s2=new Vector4(q12*Mathf.Rad2Deg,q22*Mathf.Rad2Deg,t3,q42*Mathf.Rad2Deg);
        return new Tuple<Vector4, Vector4>(s1, s2);
        }
