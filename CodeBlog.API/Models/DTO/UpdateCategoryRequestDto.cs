namespace CodeBlog.API.Models.DTO
{
    public class UpdateCategoryRequestDto
    {
        //createcategoryRequestDto da da aynı proplar var neden yeni aynı dto oluşturuyoruz ? proplar aynı olsa bile temiz kod açısından gelecekteki sorunları azaltmak için ayırıyoruz.
        public string Name { get; set; }
        public string UrlHandle { get; set; }
    }
}
