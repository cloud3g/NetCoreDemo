using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Blog.Models
{
    public interface IEntity<TKey>
    {
    }
    public abstract class Entity : IEntity<int>
    {
        [Key]    
        public virtual int Id { get; set; }
        [Display(Name = "创建时间")]
        public DateTime? CreateTime { get; set; }
        [Display(Name ="修改时间")]
        public DateTime? UpdateTime { get; set; }
    }
}
