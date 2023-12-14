using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Permission
{
    public interface IPM
    {
        /// <summary>
        /// 서버에서 데이터 받아와서 업데이트 시킬 때 사용 할 예정
        /// </summary>
        /// <param name="connect"></param>
        void D_AllAuthListChange(SAuth connect);

        /// <summary>
        /// 소켓쪽에서 데이터를 처리 할 예정
        /// </summary>
        /// <param name="connect"></param>
        void D_MyAuthListChange(SAuth connect);
    }
}
