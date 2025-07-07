# Brazil Invoice Mock

# Available services
This project provides a mock for the Brazilian Invoice services, including:
- SEFAZ - Secretaria da Fazenda:
	- NFeAutorizacao4
	- NFeRetAutorizacao4
- City Halls:
	- Ginfes - Nota Fiscal de Serviços Eletrônica (NFS-e) *just authorizion, rejection to be implemented*:
		- São Caetano do Sul
		- Santos
		- Etc.

# How to run the mock
Create an IIS application and set the path to the published folder of the project or run locally.

# How to create new services
## 1 - Get WSDL and generate interfaces
Get WSDL from the service provider and save it in the `WSDL` folder. Then run the following command to generate the interface:
```bash
wsdl WSDL/NFeAutorizacao4.wsdl /l:CS /serverInterface /out:Interfaces/INFeAutorizacao4.cs
```
Add the genertaed interface to the `Interfaces` folder. The name of the interface should be the same as the service, for example `INFeAutorizacao4.cs` for the NFeAutorizacao4 service.

## 2 - Get official XSDs and generate models
Get the XSD of the serivce request from the service provider and save it in the `XSD` folder. Depending on the service, you may need to download multiple XSD files.
If you are not sure which XSD files to use, when running the command below, it will show you the files that are required to generate the interface.
Here is an example of the minimum set of XSD files required to generate the interface for NFeAutorizacao4:
```bash
xsd XSD/enviNFe_v4.00.xsd XSD/leiauteNFe_v4.00.xsd XSD/tiposBasico_v4.00.xsd XSD/xmldsig-core-schema_v1.01.xsd /c /out:Models
```
Rename the generated file to the name of the model and import it in the `Models` folder.

## 3 - Implement the interface and create the processor
Create a new ASMX file in the WebServices folder and implement the interface generated in step 1. 
The ASMX file should be named after the service, for example `NFeAutorizacao4.asmx`.
Deserialize the request using the model generated in step 2 and return the response using a dedicated processor for the service.

# About the assets
The mock uses templates to generate the XML responses. The templates are located in the `Templates` folder and are named after the service, for example `NFeAutorizacao4.xml`.

For Brazilian Tax Authority, for example, to return the response with the correct text, JSONs are used with response based on the status code. Application version is based on the state code.