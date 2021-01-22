using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webpageTest.Models
{
    public class Excercise
    {
        public int Id { get; set; }

        [Required, DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm}", ApplyFormatInEditMode=true)]
        public DateTime Date { get; set; }

        public float Length { get; set; }

        public virtual ExcerciseType ExType { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public float Calories => ExType==null?0.0f:(int)(ExType.CalPerSec/60.0f * Length);
        
        public string Summary => ExType==null?"":$"{ExType.Name} for {Length} minutes; burned {Calories} KCal";
    }
}