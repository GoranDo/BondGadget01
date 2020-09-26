using BondGadget01.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;

namespace BondGadget01.Data
{

    internal class GadgetDAO
    {
        //connection string
        private string connectionString = @"Data Source=(localdb)\localdbG;Initial Catalog=BondGadgetDB1;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public List<GadgetModel> FetchAll()
        {
            //return list from list of gadget model
            List<GadgetModel> returnList = new List<GadgetModel>();

            //otvaramo bazu
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //radimo sql upit
                string sqlQuery = @"select * from dbo.Gadgets;";
                //kreiramo command
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                //otvaramo konekciju
                connection.Open();
                //kreiramo reader
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        //novi gadget model
                        GadgetModel gadget = new GadgetModel();
                        gadget.Id = reader.GetInt32(0);
                        gadget.Name = reader.GetString(3);
                        gadget.Description = reader.GetString(4);
                        gadget.AppearsIn = reader.GetString(2);
                        gadget.WithThisActor = reader.GetString(1);

                        //dodamo u return list
                        returnList.Add(gadget);
                    }
                }
            }
            return returnList;
        }

        public int Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                string sqlQuery = "DELETE FROM dbo.Gadgets WHERE Id = @Id";

                //kreiramo command
                SqlCommand command = new SqlCommand(sqlQuery, connection);

                command.Parameters.Add("@Id", System.Data.SqlDbType.Int).Value = id;

                //otvaramo konekciju
                connection.Open();
                //kreiramo reader
                int deletedID = command.ExecuteNonQuery();

                return deletedID;
            }
        }

        internal List<GadgetModel> SearchForDescription(string searchPhrase)
        {
            //return list from list of gadget model
            List<GadgetModel> returnList = new List<GadgetModel>();

            //otvaramo bazu
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //radimo sql upit
                string sqlQuery = @"select * from dbo.Gadgets WHERE Description LIKE @searchForMe";

                //kreiramo command
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                //slažemo pretraživanje i wildcard
                command.Parameters.Add("@searchForMe", System.Data.SqlDbType.NVarChar).Value = "%" + searchPhrase + "%";

                //otvaramo konekciju
                connection.Open();
                //kreiramo reader
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        //novi gadget model
                        GadgetModel gadget = new GadgetModel();
                        gadget.Id = reader.GetInt32(0);
                        gadget.Name = reader.GetString(3);
                        gadget.Description = reader.GetString(4);
                        gadget.AppearsIn = reader.GetString(2);
                        gadget.WithThisActor = reader.GetString(1);

                        //dodamo u return list
                        returnList.Add(gadget);
                    }
                }
                return returnList;
            }
        }

        internal List<GadgetModel> SearchForName(string searchPhrase)
        {
            //return list from list of gadget model
            List<GadgetModel> returnList = new List<GadgetModel>();

            //otvaramo bazu
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //radimo sql upit
                string sqlQuery = @"select * from dbo.Gadgets WHERE NAME LIKE @searchForMe";
                
                //kreiramo command
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                //slažemo pretraživanje i wildcard
                command.Parameters.Add("@searchForMe", System.Data.SqlDbType.NVarChar).Value = "%" + searchPhrase + "%";

                //otvaramo konekciju
                connection.Open();
                //kreiramo reader
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        //novi gadget model
                        GadgetModel gadget = new GadgetModel();
                        gadget.Id = reader.GetInt32(0);
                        gadget.Name = reader.GetString(3);
                        gadget.Description = reader.GetString(4);
                        gadget.AppearsIn = reader.GetString(2);
                        gadget.WithThisActor = reader.GetString(1);

                        //dodamo u return list
                        returnList.Add(gadget);
                    }
                }
                return returnList;
            }
            
        }

        public GadgetModel FetchOne(int Id)
        {
            //return list from list of gadget model

            //otvaramo bazu
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //radimo sql upit
                string sqlQuery = @"select * from dbo.Gadgets Where Id = @Id;";
                

                //kreiramo command
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.Parameters.Add("@Id", System.Data.SqlDbType.Int).Value = Id;
                //otvaramo konekciju
                connection.Open();
                //kreiramo reader
                SqlDataReader reader = command.ExecuteReader();
                GadgetModel gadget = new GadgetModel();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        //novi gadget model


                        gadget.Id = reader.GetInt32(0);
                        gadget.Name = reader.GetString(3);
                        gadget.Description = reader.GetString(4);
                        gadget.AppearsIn = reader.GetString(2);
                        gadget.WithThisActor = reader.GetString(1);

                        //dodamo u return list

                    }
                }
                return gadget;
            }
           
        }

        public int CreateOrUpdate(GadgetModel gadgetModel)
        {
            //return list from list of gadget model

            //otvaramo bazu
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "";
                //ukoliko je novi obrazac:
                if (gadgetModel.Id < 0)
                {
                    sqlQuery = "INSERT INTO dbo.Gadgets VALUES(@Name, @Description, @AppearsIn, @WithThisActor)";
                }
                // ukoliko je stari obrazac radimo update: 
                else
                {
                    sqlQuery = "UPDATE dbo.Gadgets SET Name = @Name, Description = @Description, AppearsIn = @AppearsIn, WithThisActor = @WithThisActor WHERE Id = @Id";
                }
                


                //kreiramo command
                SqlCommand command = new SqlCommand(sqlQuery, connection);

                command.Parameters.Add("@Id", System.Data.SqlDbType.Int).Value = gadgetModel.Id;
                command.Parameters.Add("@Name", System.Data.SqlDbType.VarChar, 1000).Value = gadgetModel.Name;
                command.Parameters.Add("@Description", System.Data.SqlDbType.VarChar, 1000).Value = gadgetModel.Description;
                command.Parameters.Add("@AppearsIn", System.Data.SqlDbType.VarChar, 1000).Value = gadgetModel.AppearsIn;
                command.Parameters.Add("@WithThisActor", System.Data.SqlDbType.VarChar, 1000).Value = gadgetModel.WithThisActor;

                //otvaramo konekciju
                connection.Open();
                //kreiramo reader
                int newID = command.ExecuteNonQuery();

                return newID;
            }

        }

    }
}