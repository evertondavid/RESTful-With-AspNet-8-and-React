using Microsoft.EntityFrameworkCore;
using RestfullWithAspNet.Data.VO;

namespace RestfullWithAspNet.Model.Context
{
    /// <summary>
    /// Represents the MySQL database context for the application.
    /// </summary>
    public class MySQLContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MySQLContext"/> class.
        /// </summary>
        public MySQLContext()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MySQLContext"/> class with the specified options.
        /// </summary>
        /// <param name="options">The options for configuring the context.</param>
        public MySQLContext(DbContextOptions<MySQLContext> options) : base(options)
        {

        }

        /// <summary>
        /// Gets or sets the collection of persons in the database.
        /// </summary>
        public DbSet<Person> Persons { get; set; }

        /// <summary>
        /// Gets or sets the collection of books in the database.
        /// </summary>
        public DbSet<Book> Books { get; set; }

        /// <summary>
        /// Gets or sets the collection of users in the database.
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Gets or sets the collection of file details in the database.
        /// </summary>
        public DbSet<FileDetailVO> FileDetails { get; set; } // Add this line
    }
}
