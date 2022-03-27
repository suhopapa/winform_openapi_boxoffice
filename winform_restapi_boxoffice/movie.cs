using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace winform_restapi_boxoffice
{
    [Browsable(false)]
    public class movie
    {

        [Display(Order = 0)]
        public int Rank { get; set; }

        [Browsable(false)]
        public int RankInten { get; set; }

        [Display(Order = 1, Name = "Name", AutoGenerateField = true)]
        public string MovieName { get; set; }

        [Display(Order = 4, Name = "Open Date")]
        public string OpenDate { get; set; }

        [Display(Order = 3, Name = "ACC")]
        public int AudiAcc { get; set; }
        
        [Display(Order = 2, Description = "Today")]
        public int AudiCnt { get; set; }


    }
}
