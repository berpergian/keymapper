﻿using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

public class Category
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }

    public Category()
    { }

    public Category(int categoryID, string categoryName, string categorySlug)
    {
        ID = categoryID;
        Name = categoryName;
        Slug = categorySlug;
    }

    public static int GetCategoryIDFromQueryString(NameValueCollection parameters)
    {

        string[] keys = parameters.AllKeys;

        int categoryID = 0;
        foreach (string key in keys)
        {
            if (key.ToUpperInvariant() == "C")
            {
                foreach (string value in parameters.GetValues(key))
                {
                    if (System.Int32.TryParse(value, out categoryID))
                    {
                        break;
                    }
                }
            }
        }
        return categoryID;
    }

    public static bool DoesCategoryExist(string name)
    {
        return DataAccess.CreateInstance().DoesCategoryExist(name) ;
    }

	public static Collection<Category> GetAllCategories()
	{
		return DataAccess.CreateInstance().GetAllCategories();
	}

	public static Collection<Category> GetCategoriesForPost(int postID)
	{
		return DataAccess.CreateInstance().GetPostByID(postID).Categories;
	}

	public static void SyncCategories(int postID, Collection<int> categories)
	{
		DataAccess.CreateInstance().SyncCategories(postID, categories);
	}

	public static bool Add(string categoryName, string categorySlug)
	{
		if (String.IsNullOrEmpty(categoryName))
			throw new NullReferenceException("Category name can't be empty") ;

		// TODO: Sort out all this slug stuff! 

        if (String.IsNullOrEmpty(categorySlug)) // Hmm. Need to check if category slug exists already, like with post
        {
            categorySlug = AppController.GetSlug(categoryName);
        }
		return DataAccess.CreateInstance().AddCategory(categoryName, categorySlug);
	}

	 public static bool Edit(Category c)
    {
		return DataAccess.CreateInstance().EditCategory(c);
    }

	public static bool Delete(int categoryID)
	{
		return DataAccess.CreateInstance().DeleteCategory(categoryID);
	}

    internal static Category GetCategoryByID(int categoryID)
    {
        return DataAccess.CreateInstance().GetCategoryByID(categoryID);
    }
}
