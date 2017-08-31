using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace KitchenService.Schema
{
    public class DayPlan
    {
        [DataMember]
        public long Id { get; set; }

        public DateTimeOffset DateTime { get; set; }

        [DataMember]
        public List<DayPlanRecipe> DayPlanRecipes { get; set; }

        public long AccountId { get; set; }
        [ForeignKey("AccountId")]
        public Account Account { get; set; }
    }
}
