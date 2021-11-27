using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;


namespace Launchpad.Models {
    public class PublicManager : DbContext {
        protected DbSet<Category> tblCategories { get; set; }
        protected DbSet<Link> tblLinks { get; set; }

        public List<Category> GetCategories() {
            List<Category> catList = tblCategories.Include("links").ToList();
            // sort the links
            foreach (Category cat in catList) {
                cat.links = cat.links.OrderBy(l => l.title).ToList();
            }
            return catList;
        }

        public int GetPinnedCount(Category category) {
            return category.links.Where(l => l.pinned).Count();
        }

        public List<Link> GetLinks() {
            return tblLinks.ToList();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
           optionsBuilder.UseMySql(Connection.CON_STRING, new MySqlServerVersion(new Version(8, 0, 11)));
        }
    }
}