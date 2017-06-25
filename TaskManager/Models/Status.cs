using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaskManager.Models 
{
    public enum Status
    {
        [Display(Name = "Активна")]
        Active,

        [Display(Name = "Остановлена")]
        Stopped,

        [Display(Name = "Выполнена")]
        Accomplished
    }
}