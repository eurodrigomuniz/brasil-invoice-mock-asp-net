# Brazil Invoice Mock

# How to create new services
Get WSDL from the service provider and save it in the `WSDL` folder. Then run the following command to generate the interface:
```bash
wsdl WSDL/NFeAutorizacao4.wsdl /l:CS /serverInterface /out:Interfaces/INFeAutorizacao4.cs
```

Get the XSD from the service provider and save it in the `XSD` folder. Depending on the service, you may need to download multiple XSD files.
If you are not sure which XSD files to use, when running the command below, it will show you the files that are required to generate the interface.
Here is an example of the minimum set of XSD files required to generate the interface for NFeAutorizacao4:
```bash
xsd XSD/enviNFe_v4.00.xsd XSD/leiauteNFe_v4.00.xsd XSD/leiauteNFe_v4.00.xsd XSD/tiposBasico_v4.00.xsd XSD/xmldsig-core-schema_v1.01.xsd /c /out:Models
```
Rename the generated file to the name of the model and import it in the `Models` folder.