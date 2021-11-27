using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Launchpad.Models {
    public class AdminMangager : PublicManager {
        public User user { get; set; }
        public EditCategoryForm editCategoryForm { get; set; }
        public LinkForm linkForm { get; set; }

        public SelectList CategorySelectList {
            get{
                return new SelectList(tblCategories.ToList(), "id", "categoryName");
            }
        }

        public int catId { get; set; }
        public int linkId { get; set; }

        public bool checkValidCatId(int id) {
            return (tblCategories.Where(cat => cat.id == id).Count() != 0);
        }

        public bool checkValidLinkId(int id) {
            return (tblLinks.Where(link => link.id == id).Count() != 0);
        }

        public string GetCategoryName(int id) {
            return tblCategories.Where(cat => cat.id == id).First().categoryName;
        }

        public void PopulateLinkForm() {
            Link link = tblLinks.Where(l => l.id == linkId).First();
            linkForm = new LinkForm {
                categoryId = link.categoryId,
                title = link.title,
                url = link.url,
                pinned = link.pinned
            };
        }

        public void EditCategory() {
            Category category = tblCategories.Where(cat => cat.id == catId).First(); // get the category
            category.categoryName = editCategoryForm.categoryName; // set the category name
            SaveChanges(); // save the changes
        }

        public void AddLink() {
            Link newLink = new Link();
            newLink.categoryId = catId;
            newLink.title = linkForm.title;
            newLink.url = linkForm.url;
            newLink.pinned = linkForm.pinned;
            tblLinks.Add(newLink);
            SaveChanges();
        }

        public void EditLink() {
            Link link = tblLinks.Where(l => l.id == linkId).First();
            link.categoryId = linkForm.categoryId;
            link.title = linkForm.title;
            link.url = linkForm.url;
            link.pinned = linkForm.pinned;
            SaveChanges();
        }

        public Link GetLink(int id) {
            return tblLinks.Where(l => l.id == id).First();
        }

        public Link GetLink() {
            return GetLink(linkId);
        }

        public void DeleteLink() {
            Link link = tblLinks.Where(l => l.id == linkId).First();
            tblLinks.Remove(link);
            SaveChanges();
        }
    }
}