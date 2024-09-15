using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Models.Common;

namespace WebApplication1.EntityFramework.Common
{
    [Index(nameof(SchemeType), IsUnique = true)]
    public class DocumentNoScheme
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public DocumentNoSchemeType SchemeType { get; set; }
        [Required]
        public long LastDocumentNo { get; set; }
        [Timestamp]
        public byte[] TimeStamp { get; set; }
    }
}
