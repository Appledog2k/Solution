// See https://aka.ms/new-console-template for more information
/*
    Search Characters Unsign :
    Tìm kiếm 1 chữ bất kì có dấu nhưng chỉ cần tìm kiếm không dấu
    ví dụ tìm kiếm đạo tạo chỉ cần search: dao tao
*/
//* Cách 1 : Sử dụng delegate trong mệnh đề where, trong delegate sẽ sử dụng hàm chuyển đổi tiếng việt sang chữ không dấu rồi so sánh
using System.Text;
using System.Text.RegularExpressions;

var query = db.Cartegories.Where(delegate (Cartegory C)
{
    if (ConvertToUnSign(C.Name).IndexOf(filter, StringComparison.CurrentCultureIgnoreCase) >= 0)
    {
        return true;
    }
    else
    {
        return false;
    }
}).AsQueryable();
/*
Hàm Convert To Unsign
*/
private string ConvertToUnSign(string input)
{
    input = input.Trim();
    for (int i = 0x20; i < 0x30; i++)
    {
        input = input.Replace(((char)i).ToString(), "");
    }
    Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");
    string str = input.Normalize(NormalizationForm.FormD);
    string str2 = regex.Replace(str, string.Empty).Replace('đ', 'd').Replace('Đ', 'D');
    while (str2.IndexOf("?") >= 0)
    {
        str2 = str2.Remove(str2.IndexOf("?"), 1);
    }
    return str2;
}
