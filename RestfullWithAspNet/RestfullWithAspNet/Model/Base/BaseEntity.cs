using System.ComponentModel.DataAnnotations.Schema;

namespace RestfullWithAspNet.Model.Base
{
    public class BaseEntity
    {
        /// <summary>
        /// Get or set the id.
        /// </summary>
        [Column("id")]
        public long Id { get; set; }
    }
}
