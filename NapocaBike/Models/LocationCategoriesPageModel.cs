using Microsoft.AspNetCore.Mvc.RazorPages;
using NapocaBike.Data;
using Microsoft.AspNetCore.Authentication;


namespace NapocaBike.Models
{
    public class LocationCategoriesPageModel : PageModel
    {
        public List<AssignedCategoryData> AssignedCategoryDataList;
        public void PopulateAssignedCategoryData(NapocaBikeContext context,
        Location location)
        {
            var allCategories = context.Category;
            var locationCategories = new HashSet<int>(
            location.LocationCategories.Select(c => c.CategoryID));
            AssignedCategoryDataList = new List<AssignedCategoryData>();
            foreach (var cat in allCategories)
            {
                AssignedCategoryDataList.Add(new AssignedCategoryData
                {
                    CategoryID = cat.ID,
                    Name = cat.CategoryName,
                    Assigned = locationCategories.Contains(cat.ID)
                });
            }
        }
        public void UpdateLocationCategories(NapocaBikeContext context,
        string[] selectedCategories, Location locationToUpdate)
        {
            if (selectedCategories == null)
            {
                locationToUpdate.LocationCategories = new List<LocationCategory>();
                return;
            }
            var selectedCategoriesHS = new HashSet<string>(selectedCategories);
            var locationCategories = new HashSet<int>
            (locationToUpdate.LocationCategories.Select(c => c.Category.ID));
            foreach (var cat in context.Category)
            {
                if (selectedCategoriesHS.Contains(cat.ID.ToString()))
                {
                    if (!locationCategories.Contains(cat.ID))
                    {
                        locationToUpdate.LocationCategories.Add(
                        new LocationCategory
                        {
                            LocationID = locationToUpdate.ID,
                            CategoryID = cat.ID
                        });
                    }
                }
                else
                {
                    if (locationCategories.Contains(cat.ID))
                    {
                        LocationCategory courseToRemove
                        = locationToUpdate
                        .LocationCategories
                        .SingleOrDefault(i => i.CategoryID == cat.ID);
                        context.Remove(courseToRemove);
                    }
                }
            }
        }


    }
}

