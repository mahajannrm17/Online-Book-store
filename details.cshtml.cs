using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using static bookstore.Pages.NewFolder2.NewFolder.IndexModel;

namespace bookstore.Pages.NewFolder2.NewFolder
{
    public class detailsModel : PageModel
    {
        private string connectString;
        private object messageInfo;

        public void OnGet()
        {
            string requestId= Request.Query["id"];
            try
            {
                string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=bookstore;Integrated Security=True";
                using (SqlConnection con = new SqlConnection(connectString))
                {
                    con.Open();
                    string sql = "SELECT * FROM messages WHERE id= @id";
                    using (SqlCommand command= new SqlCommand(sql, con))
                    {
                        command.Parameters.AddWithValue("@id", requestId);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader .Read())
                            {
                                MessageInfo messageInfo = new MessageInfo();    
                                messageInfo.Id = reader.GetInt32(0);
                                messageInfo.FirstName = reader.GetString(1);
                                messageInfo.LastName = reader.GetString(2);
                                messageInfo.Email = reader.GetString(3);
                                messageInfo.Phone = reader.GetString(4);
                                messageInfo.Subject = reader.GetString(5);
                                messageInfo.Message = reader.GetString(6);
                                messageInfo.CreatedAt = reader.GetDateTime(7).ToString("MM/dd/yyyy");
                            }
                            else
                            {
                                Response.Redirect("/NewFolder2/NewFolder/Index");
                            }
                        }
                    } 
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine (ex.Message);
                Response.Redirect("/NewFolder2/NewFolder/Index");
            }
            

            
        }

        private SqlCommand newSqlCommand(string sql, SqlConnection con)
        {
            throw new NotImplementedException();
        }
    }
}
