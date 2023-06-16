using ExamenPM02_P1_AmnerSauceda.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExamenPM02_P1_AmnerSauceda.Controllers
{
    
    public class DBProc
    {
        readonly SQLiteAsyncConnection _connection;

        public DBProc() { }

        public DBProc(string dbpath)
        {
            _connection = new SQLiteAsyncConnection(dbpath);

            _connection.CreateTableAsync<Sitio>().Wait();
        }

        //CRUD DB

        //create
        public Task<int> AddSitio(Sitio sitios)
        {
            if(sitios.Id == 0)
            {
                return _connection.InsertAsync(sitios);

            }
            else
            {
                return _connection.UpdateAsync(sitios);
            }
        }

        //Read
        public Task<List<Sitio>> GetAllSitios()
        {
            return _connection.Table<Sitio>().ToListAsync();
        }

        public Task<Sitio> GetById(int id)
        {
            return _connection.Table<Sitio>()
                .Where(i => i.Id == id)
                .FirstOrDefaultAsync();
        }

        //Delete
        public Task<int> DeleteSitio(Sitio sitios)
        {
            return _connection.DeleteAsync(sitios);
        }
    }
}
