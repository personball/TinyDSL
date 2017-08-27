# TinyDSL

### TinyDSL.Xml

Install: `Install-Package TinyDSL.Xml`  
Usage Sample:
```
    class Program
    {
        static void Main(string[] args)
        {
            var xmlWrapper =
                XDC.New("root")
                    .A
                        .B(new
                        {
                            name = "jerry",
                            path = "path/to/home"
                        })
                            .C
                                .D
                                    .E
                                    +
                                    XDC.New("F", new
                                    {
                                        name = "Tom"
                                    })
                                    .G
                                        .H
                                            .I;

            Console.WriteLine(xmlWrapper);
            Console.ReadLine();
        }
    }
```
output:
```
<root>
  <A>
    <B name="jerry" path="path/to/home">
      <C>
        <D>
          <E />
          <F name="Tom">
            <G>
              <H>
                <I />
              </H>
            </G>
          </F>
        </D>
      </C>
    </B>
  </A>
</root>
```













# LICENSE
MIT
