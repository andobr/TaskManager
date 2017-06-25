using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaskManager.Models 
{
    public class CheckListItem
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        [Display(Name = "Название")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        [Display(Name = "№")]
        public int Order { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        public int TaskId { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        [Display(Name = "Выполнено")]
        public bool Done { get; set; }

        [Display(Name = "Задача")]
        public Task Task { get; set; }
    }
}