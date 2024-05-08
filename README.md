# dotnet
关于dotnet相关的一些资料

ZRQ.Utility：

一些通用功能的绘制

## 一些准备研究的东西

### Json序列化

[如何使用 System.Text.Json 序列化派生类的属性 | Microsoft Docs](https://docs.microsoft.com/zh-cn/dotnet/standard/serialization/system-text-json-polymorphism)

[如何编写用于 JSON 序列化的自定义转换器 - .NET | Microsoft Docs](https://docs.microsoft.com/zh-cn/dotnet/standard/serialization/system-text-json-converters-how-to?pivots=dotnet-6-0#support-polymorphic-deserialization)

### XML序列化

XML序列化在多态的应用

## Test

### AttributeTest

是想解决以下情况而测试的内容：  

我有很多解析器，每种解析器可以解析不同的文件格式。  
我想将不同的文件格式，绑定到不同的解析器上，  
不想通过switch来进行判断，避免在每次增加新的解析器的时候，都会修改switch 

[AttributeTest](Test/AttributeTest/README.MD)

### ForTest

测试 For Foreach 的性能测试