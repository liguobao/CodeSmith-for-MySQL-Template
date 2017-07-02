
源自：[author:尹连冬](http://www.yldzyp001.com)（不用点了，打不开了....）

修改者：[author:李国宝](http://codelover.link:2017)


功能：


将MySQL数据库中的表生成对应的model/dal/bll。


新增：

1. Main.cst 选择数据源时，可多选；
2. 原Main.cst 改为CreatSingleTable.cst，为Main.cst提供调用源；
3. 表字段注释直接读取数据库字段注释（需要把原来SchemaExplorer.MySQLSchemaProvider.dll替换新的DLL
（E:\\CodeSmith\\v7.0\\SchemaProviders）

4. 调整model字段结构，去掉私有变量，改为属性。
5. 新增GetBycolumnName(string columnName,string columnContent)方法。




English：

1. create MySQL table to model/dal/bll

2. Main.cst： select many table to create model/dal/bll

3. replace SchemaExplorer.MySQLSchemaProvider.dll(as... E:\\CodeSmith\\v7.0\\SchemaProviders)，the template can read table comment and column comment. 

4. MySQLSchemaProvider.7z is the sln ,to create SchemaExplorer.MySQLSchemaProvider.dll





