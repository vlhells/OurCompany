using System.ComponentModel.DataAnnotations;

namespace Practice_1.Areas.Admin.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? Role { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public int Age { get; set; }
    }

	//public class Role
	//{
 //       [Key]
	//	public string Name { get; set; }
	//	public Role(string name) => Name = name;
 //       public Role()
 //       {

 //       }
	//}
}
