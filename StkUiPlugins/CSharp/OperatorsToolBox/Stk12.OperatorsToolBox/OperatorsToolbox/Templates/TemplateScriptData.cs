using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperatorsToolbox.Templates
{
    public class TemplateScriptData
    {
        public bool PreImportScriptActive { get; set; }
        public string PreImportScriptPath { get; set; }
        public string PreImportArgs { get; set; }
        public bool PostImportScriptActive { get; set; }
        public string PostImportScriptPath { get; set; }
        public string PostImportArgs { get; set; }
    }
}
