using NPOI.HSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Tools
{
   public class WorkBook
    {
        public static HSSFWorkbook BuildSwitchData<T>(string SheetName, List<T> list, Dictionary<string, string> FiedNames)
        {
            HSSFWorkbook wb = new HSSFWorkbook();
            HSSFSheet sheet = (HSSFSheet)wb.CreateSheet(SheetName); //创建工作表
            sheet.CreateFreezePane(0, 1); //冻结列头行
            HSSFRow row_Title = (HSSFRow)sheet.CreateRow(0); //创建列头行
            row_Title.HeightInPoints = 30.5F; //设置列头行高
            HSSFCellStyle cs_Title = (HSSFCellStyle)wb.CreateCellStyle(); //创建列头样式
            cs_Title.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center; //水平居中
            cs_Title.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center; //垂直居中
            HSSFFont cs_Title_Font = (HSSFFont)wb.CreateFont(); //创建字体
            cs_Title_Font.IsBold = true; //字体加粗
            cs_Title_Font.FontHeightInPoints = 14; //字体大小
            cs_Title.SetFont(cs_Title_Font); //将字体绑定到样式
            #region 生成列头
            int ii = 0;
            foreach (string key in FiedNames.Keys)
            {
                HSSFCell cell_Title = (HSSFCell)row_Title.CreateCell(ii); //创建单元格
                cell_Title.CellStyle = cs_Title; //将样式绑定到单元格
                cell_Title.SetCellValue(key);
                sheet.SetColumnWidth(ii, 25 * 256);//设置列宽
                ii++;
            }

            #endregion
            //获取 实体类 类型对象
            Type t = typeof(T); // model.GetType();
            //获取 实体类 所有的 公有属性
            List<PropertyInfo> proInfos = t.GetProperties(BindingFlags.Instance | BindingFlags.Public).ToList();
            //创建 实体属性 字典集合
            Dictionary<string, PropertyInfo> dictPros = new Dictionary<string, PropertyInfo>();
            //将 实体属性 中要修改的属性名 添加到 字典集合中 键：属性名  值：属性对象
            proInfos.ForEach(p =>
            {
                if (FiedNames.Values.Contains(p.Name))
                {
                    dictPros.Add(p.Name, p);
                }
            });

            HSSFCellStyle cs_Content = (HSSFCellStyle)wb.CreateCellStyle(); //创建列头样式
            cs_Content.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center; //水平居中
            cs_Content.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center; //垂直居中
            for (int i = 0; i < list.Count; i++)
            {

                HSSFRow row_Content = (HSSFRow)sheet.CreateRow(i + 1); //创建行
                row_Content.HeightInPoints = 20;
                int jj = 0;
                foreach (string proName in FiedNames.Values)
                {
                    if (dictPros.ContainsKey(proName))
                    {
                        HSSFCell cell_Conent = (HSSFCell)row_Content.CreateCell(jj); //创建单元格
                        cell_Conent.CellStyle = cs_Content;

                        //如果存在，则取出要属性对象
                        PropertyInfo proInfo = dictPros[proName];
                        //获取对应属性的值
                        object value = proInfo.GetValue(list[i], null); //object newValue = model.uName;
                        string cell_value = value == null ? "" : value.ToString();
                        cell_Conent.SetCellValue(cell_value);
                        jj++;
                    }
                }
            }
            return wb;

        }
    }
}
