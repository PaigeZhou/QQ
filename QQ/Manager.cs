using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
namespace QQ
{
    /// <summary>
    /// qq管理类
    /// </summary>
    class Manager
    {
       public void Dl()
        {
            Console.WriteLine("========================欢迎使用扣扣管理员登录系统========================");
            for (int i = 1; i <=3; i++)
            {

            Console.Write("\n\t\t\t请输入您的用户名：");
            string name = Console.ReadLine();
            Console.Write("\n\t\t\t请输入您的密码：");
            string pwd = Console.ReadLine();
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(pwd))
            {
                Console.WriteLine("\n用户名或密码不能为空值！请重试");
                    continue;
            }
                else
                {
                    string sql = string.Format("SELECT COUNT(0) FROM Admin WHERE UserName='{0}'AND Password='{1}'", name,pwd);

                    int res = Convert.ToInt32( DBHelper.ExecuteScalar(sql));

                    if (res >= 1)
                    {
                        if (name.Equals("bao"))
                        {
                        Console.WriteLine("\n欢迎您，尊贵的扣扣管理员：宝");
                        }else if(name.Equals("bei")){
                            Console.WriteLine("\n欢迎您，尊贵的扣扣管理员：贝");
                        }
                        else
                        {
                            Console.WriteLine("\n\t\t\t欢迎您，尊贵的扣扣管理员：小方");
                        }
                        Zcd();
                        return;
                    }
                    else
                    {
                        Console.WriteLine("\n您的账号或密码有误，请稍后再试");
                    }
                }
            }
            Console.WriteLine("您的三次登录机会已使用完，请明天再来光临！");

        }

        private void Zcd()
        {
            Console.WriteLine("\n========================欢迎登录扣扣用户信息管理系统========================");
            Console.WriteLine("\n-----------------------------请选择菜单项-----------------------------------");
            Console.WriteLine("\n\t\t\t1.显示用户清单");
            Console.WriteLine("\n\t\t\t2.更新在线天数");
            Console.WriteLine("\n\t\t\t3.添加用户新纪录");
            Console.WriteLine("\n\t\t\t4.更新用户等级");
            Console.WriteLine("\n\t\t\t5.删除用户记录");
            Console.WriteLine("\n\t\t\t0.退出");
            Console.WriteLine("\n============================================================================");
            Console.Write("\n\t\t\t高贵的，优雅的管理员，请选择您要进行的功能：");
            int res = Convert.ToInt32(Console.ReadLine());
            switch (res)
            {
                case 1:
                    Console.WriteLine("\n\t\t\t正在显示用户清单");
                    Xs();
                    break;
                case 2:
                    Console.WriteLine("\n\t\t\t正在更新在线天数");
                    Gxbyts();
                    break;
                case 3:
                    Console.WriteLine("\n\t\t\t正在添加用户等级");
                    Tj();
                    break;
                case 4:
                    Console.WriteLine("\n\t\t\t正在更新用户等级");
                    Gxbydj();
                    break;
                case 5:
                    Console.WriteLine("\n\t\t\t正在删除用户记录");
                    Sc();
                    break;
                case 0:
                    Console.WriteLine("\n\t\t\t已退出");
                    return;
                default:
                    Console.WriteLine("尊敬的，高贵的管理员，没有此选项！默认臣就退下了");
                    return;
            }
            Console.Write("\n\t\t\t按任意键返回主菜单：");
            string ry = Console.ReadLine();
            Zcd();
        }

        /// <summary>
        /// 正在显示用户清单
        /// </summary>
        private void Xs()
        {
            string sql = string.Format("SELECT Q.ID,Q.NickName,L.LevelName,Q.Email,Q.OnLineDay FROM QQUser Q INNER JOIN Level L ON Q.LevelID = L.LevelID");
            SqlDataReader s =  DBHelper.ExecuteReader(sql);
            Console.WriteLine("\n-----------------------------------------------------------------------------");
            Console.WriteLine("\n编号\t昵称\t\t等级\t邮箱\t\t在线天数");
            Console.WriteLine("\n-----------------------------------------------------------------------------");
            while (s.Read()&&s!=null&&s.HasRows)
            {
                string bh = s["ID"].ToString();
                string nc = s["NickName"].ToString();
                string dj = s["LevelName"].ToString();
                string yx = s["Email"].ToString();
                string ts = s["OnLineDay"].ToString();
                Console.WriteLine("\n{0}\t{1}\t\t{2}\t{3}\t\t{4}",bh,nc,dj,yx,ts);
            }
            if (s != null)
            {
                s.Close();
            }
        }

        /// <summary>
        /// 更新在线天数
        /// </summary>
        private void Gxbyts()
        {
            Console.Write("\n\t\t\t请输入用户编号：");
            int bh1 =Convert.ToInt32( Console.ReadLine());
            string sql1 =string.Format("SELECT COUNT(0) FROM QQUser WHERE ID={0} ",bh1);
            int res = Convert.ToInt32(DBHelper.ExecuteScalar(sql1));
            if (res >= 1)
            {
                Console.Write("\n\t\t\t请输入新的在线天数：");
                int ts = Convert.ToInt32(Console.ReadLine());
                string sql2 = string.Format("UPDATE QQUser SET OnLineDay={0}WHERE ID={1}",ts,bh1);
                bool pd = DBHelper.ExecuteNonQuery(sql2);
                if (pd)
                {
                    Console.WriteLine("\n\t\t\t用户在线天数更新完必！");
                }
                else
                {
                    Console.WriteLine("\n\t\t\t更新失败！");
                }
            }
            else
            {
                Console.WriteLine("\n\t\t\t高贵的管理员，此用户不存在！请您待会再试！");
            }
            
        }

        /// <summary>
        /// 添加用户等级
        /// </summary>
        private void Tj()
        {
            Console.Write("\n\t\t\t请输入用户昵称：");
            string name = Console.ReadLine();
            Console.Write("\n\t\t\t请输入用户密码：");
            string pwd = Console.ReadLine();
            string sql = string.Format("SELECT COUNT(0) FROM QQUser WHERE NickName='{0}' AND Password='{1}'", name,pwd);
            int res = Convert.ToInt32(DBHelper.ExecuteNonQuery(sql));
            if (res>0)
            {
                Console.WriteLine("\n\t\t\t尊敬的管理员，你要添加的用户已经存在，您可无需重复添加了！");
            }
            else
            {
                 string res3 = yam();
                Console.Write("\n\t\t\t请输入用户邮箱：");
                string yx = Console.ReadLine();
                string sql2 = string.Format("INSERT INTO QQUser (NickName,Password,Email,QQID)VALUES('{0}','{1}','{2}','{3}')", name,pwd,yx,res3);
                bool pd = DBHelper.ExecuteNonQuery(sql2);
                if (pd)
                {
                    string sql3 = string.Format("SELECT ID FROM QQUser WHERE NickName='{0}' AND Password='{1}'", name, pwd);
                    SqlDataReader c = DBHelper.ExecuteReader(sql3);
                    while (c!=null && c.HasRows && c.Read())
                    {
                        string s = c["ID"].ToString();
                        Console.WriteLine("\n\t\t\t插入成功，用户编号是：" + s);
                    }
                    
                }
                else
                {
                    Console.WriteLine("\n\t\t\t插入失败，请稍后再试！");
                }
                
            }
        }

        /// <summary>
        /// 更新用户等级
        /// </summary>
        private void Gxbydj()
        {
            int count = 0;
            string sql = string.Format("SELECT ID,LevelID,OnLineDay FROM QQUser");
            SqlDataReader s = DBHelper.ExecuteReader(sql);
            while (s!=null && s.Read() && s.HasRows)
            {
                int id = Convert.ToInt32(s["ID"]);
                int bh = Convert.ToInt32(s["LevelID"]);
                int dj = Convert.ToInt32(s["OnLineDay"]);
                int d = 0;
                if (dj<5)
                {
                    d = 1;
                }else if(dj>=5 && dj <= 32)
                {
                    d = 2;
                }else if (dj>=32 &&dj<=320)
                {
                    d = 3;
                }else if (dj > 320)
                {
                    d = 4;
                }
                if (d == bh)
                {
                    continue;
                }
                count++;
                string sql1 = string.Format("UPDATE QQUser SET LevelID={0}WHERE ID={1}",d,id);
                bool pd = DBHelper.ExecuteNonQuery(sql1);
                if (pd)
                {
                    count++;
                }
            }
            Console.WriteLine("\n\t\t\t本次共更新用户记录数："+count);
        }

        /// <summary>
        /// 删除用户记录
        /// </summary>
        private void Sc()
        {
            Console.Write("\n\t\t\t请输入删除的用户编号：");
            int bh =Convert.ToInt32( Console.ReadLine());

            string sql = string.Format("SELECT COUNT(0) FROM QQUser WHERE ID={0}",bh);
            int res = Convert.ToInt32(DBHelper.ExecuteScalar(sql));
            if (res > 0)
            {
                string sql1 = string.Format("SELECT Q.ID,Q.NickName,L.LevelName,Q.Email,Q.OnLineDay FROM QQUser Q INNER JOIN Level L ON Q.LevelID = L.LevelID WHERE Q.ID={0}",bh);
                SqlDataReader s = DBHelper.ExecuteReader(sql1);
                Console.WriteLine("\n\t\t\t将要删除的用户信息是：");
                Console.WriteLine("\n-----------------------------------------------------------------------------");
                Console.WriteLine("\n编号\t昵称\t\t等级\t邮箱\t\t在线天数");
                Console.WriteLine("\n-----------------------------------------------------------------------------");
                while (s.Read() && s != null && s.HasRows)
                {
                    string bh1 = s["ID"].ToString();
                    string nc = s["NickName"].ToString();
                    string dj = s["LevelName"].ToString();
                    string yx = s["Email"].ToString();
                    string ts = s["OnLineDay"].ToString();
                    Console.WriteLine("\n{0}\t{1}\t\t{2}\t{3}\t\t{4}", bh1, nc, dj, yx, ts);
                }
                Console.WriteLine("\n\t\t\t输入y继续操作：");
                string p = Console.ReadLine();
                if (p.ToLower().Equals("y"))
                {
                    string sql2 = string.Format("DELETE FROM QQUser WHERE ID={0}",bh);
                    bool pd = DBHelper.ExecuteNonQuery(sql2);
                    if (pd)
                    {
                        Console.WriteLine("\n\t\t\t删除成功!");
                    }
                    else
                    {
                        Console.WriteLine("\n\t\t\t删除失败！");
                    }
                    
                }
                else
                {
                    Console.WriteLine("\n\t\t\t用户自行退出，操作取消");
                }

            }
            else
            {
                Console.WriteLine("\n\t\t\t没有找到该用户！");
            }
        }

        /// <summary>
        /// 生成9个随机数
        /// </summary>
        /// <returns>6位随机数</returns>
        private string yam()
        {
            return new Random().Next(100000, 999999).ToString();
        }
    }
}
// Console.WriteLine("\n\t\t\t");
