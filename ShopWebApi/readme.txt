Controllers里面放的是控制器 Weather那个不用管他 自带的
Model里面放的是实体模型
你要改到你自己那边的话 首先把连接字符串改一下 在appsettings.json里面 两个都改了
然后在package manager console里面
Add-Migration initmigration //添加一个迁移文件
Update-database  //更新数据库
