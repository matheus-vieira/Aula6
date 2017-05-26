using Model.Tables;

namespace Aula6.Models
{
    public class CategoryListAPIModel : APIModel
    {
        // { Message: "OK", Result: [{},{}] }
        public System.Collections.Generic.List<Category> Result
        { get; set; }
    }

    public class CategoryAPIModel : APIModel
    {
        // { Message: "OK", Result: {} }
        public Category Result
        { get; set; }
    }
}