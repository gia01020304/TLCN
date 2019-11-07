using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Formatters.Internal;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace General
{
    public static class CoreExtension
    {
        public static void AddApplicationError(this HttpResponse resp, string message)
        {
            resp.Headers.Add("Application-Error", message);
            resp.Headers.Add("Access-Control-Expose-Headers", "Application-Error");
            resp.Headers.Add("Access-Control-Allow-Origin", "*");
        }
        public static async System.Threading.Tasks.Task<string> GenerateJwtTokenAsync(ApplicationUser model, UserManager<ApplicationUser> userManager, string secret)
        {
            var claim = new List<Claim>
            {
                new Claim("id",model.Id.ToString()),
                new Claim("userName",model.UserName),
                new Claim("imagePath",model.ImagePath??string.Empty)
            };
            var rolesOfUser = await userManager.GetRolesAsync(model);
            foreach (var role in rolesOfUser)
            {
                claim.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claim),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        //public static void AddPagination(this HttpResponse resp, int currentPage, int itemsPerPage, int totalItems, int totalPages)
        //{
        //    var pagination = new PaginationHeader(currentPage, itemsPerPage, totalItems, totalPages);
        //    var calmelCaseFormatter = new JsonSerializerSettings();
        //    calmelCaseFormatter.ContractResolver = new CamelCasePropertyNamesContractResolver();

        //    resp.Headers.Add("Pagination", JsonConvert.SerializeObject(pagination, calmelCaseFormatter));
        //    resp.Headers.Add("Access-Control-Expose-Headers", "Pagination");

        //}
        public static string CreateMessageLog(this object objectClass, string message)
        {
            var st = new StackTrace();
            var sf = st.GetFrame(1);
            return string.Format("{0}-{1}:{2}", objectClass.GetType().Name, sf.GetMethod().Name, message);
        }
        public static string GetUserId(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
        public static string GetMessage(this ModelStateDictionary modelState)
        {
            return modelState.Select(x => x.Value.Errors)
                            .Where(y => y.Count > 0)
                            .ToList().GetMessage();
        }
        private static string GetMessage(this List<ModelErrorCollection> lError)
        {
            string msg = string.Empty;
            lError.ForEach(x => msg += x.First().ErrorMessage + "<br/>");
            return msg.Remove(msg.Length - 5);
        }
        public static string GetMessage(this IEnumerable<IdentityError> lError)
        {
            string msg = string.Empty;
            foreach (var item in lError)
            {
                msg += item.Description;
            }
            return msg;
        }

        public static T To<T>(this DataRow row)
        {
            var obj = (T)Activator.CreateInstance(typeof(T));
            try
            {
                if (row != null)
                {
                    foreach (PropertyInfo p in obj.GetType().GetProperties())
                    {
                        if (p.CanWrite)
                        {
                            if (row.Table.Columns.Contains(p.Name) && row[p.Name] != null)
                            {
                                if (p.PropertyType == typeof(decimal) || p.PropertyType == typeof(decimal?))
                                {
                                    p.SetValue(obj, decimal.Parse(row[p.Name].ToString()), null);
                                }
                                else if (p.PropertyType == typeof(double) || p.PropertyType == typeof(double?))
                                {
                                    p.SetValue(obj, double.Parse(row[p.Name].ToString()), null);
                                }
                                else if (p.PropertyType == typeof(bool) || p.PropertyType == typeof(bool?))
                                {
                                    if (row[p.Name] == null)
                                        p.SetValue(obj, false, null);
                                    else
                                    {
                                        if (row[p.Name].ToString().Equals("1"))
                                            p.SetValue(obj, true, null);
                                        else if (row[p.Name].ToString().Equals("0"))
                                            p.SetValue(obj, false, null);
                                        else
                                            p.SetValue(obj, bool.Parse(row[p.Name].ToString()), null);
                                    }
                                }
                                else if (p.PropertyType == typeof(int) || p.PropertyType == typeof(int?))
                                {
                                    p.SetValue(obj, int.Parse(row[p.Name].ToString()), null);
                                }
                                else if (p.PropertyType == typeof(DateTime) || p.PropertyType == typeof(DateTime?))
                                {
                                    if (string.IsNullOrEmpty(row[p.Name].ToString()) && p.PropertyType == typeof(DateTime?))
                                    {
                                        p.SetValue(obj, null, null);
                                    }
                                    else
                                        p.SetValue(obj, DateTime.Parse(row[p.Name].ToString()), null);
                                }
                                else if (p.PropertyType == typeof(string) || p.PropertyType == typeof(String))
                                {
                                    //2019/07/04 gnguyen start mod
                                    if (row[p.Name] == null || row[p.Name] == DBNull.Value) p.SetValue(obj, string.Empty, null);
                                    else p.SetValue(obj, row[p.Name].ToString(), null);
                                    //2019/07/04 gnguyen end mod

                                }
                                else
                                {
                                    p.SetValue(obj, row[p.Name], null);
                                }
                            }
                            else
                            {
                                p.SetValue(obj, null, null);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return obj;
        }
        public static List<TDestination> To<TDestination>(this DataTable sourceList)
        {
            List<TDestination> ret = new List<TDestination>();
            foreach (DataRow row in sourceList.Rows)
            {
                TDestination t = row.To<TDestination>();
                ret.Add(t);
            }
            return ret;
        }

        public static void DeleteFile(string fileName)
        {
            try
            {
                if (!string.IsNullOrEmpty(fileName))
                {
                    if (fileName.Contains("no-image"))
                    {
                        return;
                    }
                    var iVersion = fileName.IndexOf("?version=");
                    var numDel = fileName.Length - iVersion;
                    fileName = fileName.Remove(iVersion, numDel);

                    if (File.Exists(fileName))
                    {
                        File.Delete(fileName);
                    }
                }
            }
            catch (System.Exception ex)
            {
                CoreLogger.Instance.Error(ex);
            }
        }
        public static string StripHTML(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }
            return Regex.Replace(input, "<.*?>", String.Empty);
        }
        public static string ConvertTo64(this IFormFile file)
        {
            try
            {
                if (file != null && file.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        file.CopyTo(stream);
                        return "data:image/jpeg;base64," + Convert.ToBase64String(stream.ToArray());
                    }
                }
            }
            catch (System.Exception ex)
            {
                CoreLogger.Instance.Error(ex);
            }
            return null;
        }
        public static string SaveFile(string folder, string absolute, IFormFile file, string fileName = null)
        {
            if (file == null || file.Length == 0)
            {
                return null;
            }
            string pathDB = string.Empty;
            try
            {
                folder = folder + absolute;
                if (!Directory.Exists(folder))
                {
                    DirectoryInfo di = Directory.CreateDirectory(folder);
                }

                if (string.IsNullOrEmpty(fileName))
                {
                    fileName = DateTime.Now.Ticks.ToString();
                }
                fileName = Path.Combine(string.Concat(fileName, Path.GetExtension(file.FileName)));
                string filePath = Path.Combine(folder, fileName);
                using (var fs = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fs);
                }
                pathDB = string.Format("{0}/{1}?version={2}", absolute, fileName, DateTime.Now.Ticks.ToString().Substring(0, 8));
            }
            catch (Exception ex)
            {
                CoreLogger.Instance.Error(ex);
            }
            return pathDB;
        }
    }
}
