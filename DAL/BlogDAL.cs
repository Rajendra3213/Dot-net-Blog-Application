using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Blogify.Models;

namespace Blogify.DAL
{
    public class BlogDAL(IConfiguration configuration)
    {
        private readonly string _connectionString = configuration.GetConnectionString("DefaultConnection");

        public List<Post> GetAllPosts()
        {
            List<Post> posts = new List<Post>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT p.*, u.UserId, u.Username, u.Email FROM Posts p INNER JOIN Users u ON p.UserId = u.UserId";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Post post = new Post
                    {
                        PostId = Convert.ToInt32(reader["PostId"]),
                        Title = reader["Title"].ToString(),
                        Content = reader["Content"].ToString(),
                        CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                        UserId = Convert.ToInt32(reader["UserId"]),
                        User = new User
                        {
                            UserId = Convert.ToInt32(reader["UserId"]),
                            Username = reader["Username"].ToString(),
                            Email = reader["Email"].ToString(),
                            Posts = new List<Post>(),
                            Comments = new List<Comment>()
                        },
                        Comments = new List<Comment>()
                    };
                    posts.Add(post);
                }
            }

            return posts;
        }

        public void AddPost(Post post)
        {
            using (SqlConnection connection = new(_connectionString))
            {
                string query = "INSERT INTO Posts (Title, Content, CreatedAt, UserId) VALUES (@Title, @Content, @CreatedAt, @UserId)";
                SqlCommand command = new(query, connection);
                command.Parameters.AddWithValue("@Title", post.Title);
                command.Parameters.AddWithValue("@Content", post.Content);
                command.Parameters.AddWithValue("@CreatedAt", post.CreatedAt);
                command.Parameters.AddWithValue("@UserId", post.UserId);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void UpdatePost(Post post)
        {
            using (SqlConnection connection = new(_connectionString))
            {
                string query = "UPDATE Posts SET Title = @Title, Content = @Content, CreatedAt = @CreatedAt, UserId = @UserId WHERE PostId = @PostId";
                SqlCommand command = new(query, connection);
                command.Parameters.AddWithValue("@PostId", post.PostId);
                command.Parameters.AddWithValue("@Title", post.Title);
                command.Parameters.AddWithValue("@Content", post.Content);
                command.Parameters.AddWithValue("@CreatedAt", post.CreatedAt);
                command.Parameters.AddWithValue("@UserId", post.UserId);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void DeletePost(int postId)
        {
            using (SqlConnection connection = new(_connectionString))
            {
                string query = "DELETE FROM Posts WHERE PostId = @PostId";
                SqlCommand command = new(query, connection);
                command.Parameters.AddWithValue("@PostId", postId);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public Post? GetPostById(int id)
{
    Post post = null;

    using (SqlConnection connection = new SqlConnection(_connectionString))
    {
        string query = "SELECT p.*, u.UserId, u.Username, u.Email FROM Posts p INNER JOIN Users u ON p.UserId = u.UserId WHERE p.PostId = @PostId";
        SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@PostId", id);
        connection.Open();
        SqlDataReader reader = command.ExecuteReader();

        if (reader.Read())
        {
            post = new Post
            {
                PostId = Convert.ToInt32(reader["PostId"]),
                Title = reader["Title"].ToString(),
                Content = reader["Content"].ToString(),
                CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                UserId = Convert.ToInt32(reader["UserId"]),
                User = new User
                {
                    UserId = Convert.ToInt32(reader["UserId"]),
                    Username = reader["Username"].ToString(),
                    Email = reader["Email"].ToString(),
                    Posts = new List<Post>(),
                    Comments = new List<Comment>()
                },
                Comments = new List<Comment>()
            };
        }
    }

    return post;
}
    }
}