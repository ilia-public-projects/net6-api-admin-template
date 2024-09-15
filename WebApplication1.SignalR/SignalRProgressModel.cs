using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.SignalR
{
    public class SignalRProgressModel
    {
        public SignalRProgressModel()
        {
        }

        public SignalRProgressModel(int currentEntry, int totalCount, SignalRProgressTypeEnum progress)
        {
            Progress = currentEntry / totalCount * 100;
            ProgressTypeName = progress;
        }

        public decimal Progress { get; set; }
        public SignalRProgressTypeEnum ProgressTypeName { get; set; }
        public int ProgressType => (int)ProgressTypeName;


    }
}
