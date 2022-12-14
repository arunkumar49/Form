//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SearchForm.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public partial class UserData
    {
		public int userId { get; set; }
		[Required(ErrorMessage = "Username is required")]
		public string username { get; set; }
		[Required(ErrorMessage = "Password is required")]
		public string password { get; set; }
		[NotMapped]
		[Required(ErrorMessage = "Confirmation is required")]
		[Compare("password", ErrorMessage = "Password does not match")]
		public string ConfirmPassword { get; set; }
	}
}
