using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Permission
{
    public struct SAuth
    {
        public string AuthName;     //기능 이름
        public int AuthLevel;       //기능 권한 레벨
        public bool IsEnable;       //기능 사용가능유무
        public int IsShow;        //0 = 보임 / 2 = 안보임

    };
}
