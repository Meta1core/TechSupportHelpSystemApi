using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TechSupportHelpSystem.DAL;
using TechSupportHelpSystem.Models;

namespace TechSupportHelpSystem.Repositories
{
    public class CashRepository : IRepository<CashSchedule>
    {

        public CashSchedule Create(DbContextOptions clientOptions, CashSchedule item)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext(clientOptions))
                {
                    db.Cash_Fee_Schedule.Add(item);
                    db.SaveChanges();
                }
                return item;
            }
            catch (DbUpdateException e)
            {
                throw new DbUpdateException(e.Message);
            }
        }

        public CashSchedule Delete(DbContextOptions clientOptions, int id)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext(clientOptions))
                {
                    CashSchedule cashFromDatabase = db.Cash_Fee_Schedule.Where(c => c.ID_CashSchedule == id).FirstOrDefault();
                    cashFromDatabase.IsHidden = true;
                    db.SaveChanges();
                    return cashFromDatabase;
                }
            }
            catch (DbUpdateException e)
            {
                throw new DbUpdateException(e.Message);
            }
        }

        public CashSchedule Get(DbContextOptions clientOptions, int id)
        {
            try
            {
                CashSchedule cashFromDatabase;
                using (ApplicationContext db = new ApplicationContext(clientOptions))
                {
                    cashFromDatabase = db.Cash_Fee_Schedule.Where(c => c.ID_CashSchedule == id).FirstOrDefault();
                }
                return cashFromDatabase;
            }
            catch (ArgumentNullException e)
            {
                throw new ArgumentNullException(e.Message);
            }
        }

        public List<CashSchedule> GetAll(DbContextOptions clientOptions)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext(clientOptions))
                {
                    return db.Cash_Fee_Schedule.ToList();
                }
            }
            catch (ArgumentNullException e)
            {
                throw new ArgumentNullException(e.Message);
            }
        }

        public CashSchedule Update(DbContextOptions clientOptions, CashSchedule item)
        {
            try
            {
                CashSchedule cashFromDatabase;
                using (ApplicationContext db = new ApplicationContext(clientOptions))
                {
                    cashFromDatabase = db.Cash_Fee_Schedule.Where(r => r.ID_CashSchedule == item.ID_CashSchedule).FirstOrDefault();
                    cashFromDatabase.IsHidden = item.IsHidden;
                    cashFromDatabase.Name = item.Name;
                    db.SaveChanges();
                }
                return cashFromDatabase;
            }
            catch (DbUpdateException e)
            {
                throw new DbUpdateException(e.Message);
            }
        }
    }
}
