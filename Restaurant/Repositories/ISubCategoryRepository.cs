using Restaurant.MVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.MVC.Repositories
{
     public interface ISubCategoryRepository
    {
        SubCategoryVm GenerateSubCategoryViewModel();
    }
}
