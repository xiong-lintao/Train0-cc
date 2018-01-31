using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{

    class Class2
    {
        [Serializable]
        public struct Package
        {
            public int packageContent;//包的内容
            public int DataTime; ////时间戳
            public int Signer;//标识符
            public string PackageName;//包名
            public string SendDirection;//发送方向
            internal  void SetPackageContent(int dataTime, int singer, string name, string sendDirection)
            {
                this.DataTime = dataTime;
                this.Signer = singer;
                this.PackageName = name;
                this.SendDirection = sendDirection;
            }
            public override string ToString() { return DataTime + "," + Signer + "," + PackageName + "," + SendDirection; }

            public static implicit operator Package(TrainInit v)
            {
                throw new NotImplementedException();
            }
        }
    }
}
