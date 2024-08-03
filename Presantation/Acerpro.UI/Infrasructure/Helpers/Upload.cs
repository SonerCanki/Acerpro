namespace Acerpro.UI.Infrasructure.Helpers
{
    public class Upload
    {
        public static string ImageUpload(List<IFormFile> files,IWebHostEnvironment _env, out bool result)
        {
            result = false;
            var uploads = Path.Combine(_env.WebRootPath, "uploads");
            foreach (var file in files)
            {
                if (file.ContentType.Contains("image"))
                {
                    if (file.Length <= 2097152)
                    {
                        string uniqueName = $"{Guid.NewGuid().ToString().Replace("-", "_").ToLower()}.{file.ContentType.Split("/")[1]}";
                        var filePath = Path.Combine(uploads, uniqueName);
                        using (var fileStream = new FileStream(filePath,FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                            result = true;
                            return filePath.Substring(filePath.IndexOf("\\uploads\\"));
                        }
                    }
                    else
                    {
                        return "2MB'dan büyük boyutta resim yükleyemezsiniz!...";
                    }
                }
                else
                {
                    return "Lütfen sadece resim dosyası yükleyiniz!....";
                }
            }
            return "Dosya bulunumadı!...Lütfen en az bir resim dosyası yükleyiniz....";
        }
    }
}
