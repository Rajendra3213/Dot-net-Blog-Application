using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class User
{
    public int UserId { get; set; }
    
    [Required]
    [StringLength(50)]
    public string Username { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    // Other user properties like password, profile picture, etc. can be added here
    
    public ICollection<Post> Posts { get; set; }
    public ICollection<Comment> Comments { get; set; }
}

public class Post
{
    public int PostId { get; set; }
    
    [Required]
    [StringLength(100)]
    public string Title { get; set; }
    
    [Required]
    public string Content { get; set; }
    
    [Required]
    public DateTime CreatedAt { get; set; }
    
    public int UserId { get; set; } // Foreign key to link with User
    public User User { get; set; } // Navigation property
    
    public ICollection<Comment> Comments { get; set; }
}

public class Comment
{
    public int CommentId { get; set; }
    
    [Required]
    public string Content { get; set; }
    
    [Required]
    public DateTime CreatedAt { get; set; }
    
    public int UserId { get; set; } // Foreign key to link with User
    public User User { get; set; } // Navigation property
    
    public int PostId { get; set; } // Foreign key to link with Post
    public Post Post { get; set; } // Navigation property
}
