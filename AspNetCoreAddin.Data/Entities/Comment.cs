using AspNetCoreAddin.Data.ViewModels;
using AspNetCoreAddin.Infrastructure.SharedKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetCoreAddin.Data.Entities
{
    [Table("Comments")]
    public class Comment : DomainEntity<int>
    {
        public Comment()
        {
        }

        public Comment(CommentViewModel commentVm)
        {
            ProductId = commentVm.ProductId;
            NameCustomer = commentVm.NameCustomer;
            Email = commentVm.Email;
            Content = commentVm.Content;
            StarPoint = commentVm.StarPoint;
        }
        public int ProductId { set; get; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
        public int StarPoint { set; get; }

        [MaxLength(255)]
        [Required]
        public string NameCustomer { set; get; }

        [MaxLength(255)]
        [Required]
        public string Email { set; get; }

        [MaxLength(500)]
        public string Content { set; get; }
    }
}