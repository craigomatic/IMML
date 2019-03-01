IMML
====

### Immersive Media Markup Language

_Abstract, lightweight, flexible_

IMML aims to be a lightweight, lean and mean specification that supports spatial presentation of media, without being tied to any particular format or device. 

To achieve this, IMML provides an abstract series of elements as its foundation that represent common media types including 3d models, primitive models (box, sphere, etc), sounds, text, video and images.

All elements are defined within an [xsd file](src/Imml/imml.xsd) that can be used to validate markup in any editor that supports xsd files, such as Visual Studio:

![vs-associate-xsd](https://user-images.githubusercontent.com/146438/53659003-300f5d00-3c0f-11e9-904a-4d4c549887ab.gif)





Visit the [wiki](https://github.com/craigomatic/IMML/wiki) for code samples that show how to use the library.

#### Examples

##### Hello world with an image
```xml
	<IMML Camera="Camera" xmlns="http://schemas.vastpark.com/2007/imml/">
		<Camera Name="Camera" Position="0,0,-5" />
		
		<Primitive Type="Box">
			<MaterialGroup Id="-1">
				<Texture Source="hello-world.jpg" />					
			</MaterialGroup>
		</Primitive>
	</IMML>
```	
##### Stack of elements

In addition to elements representing various media types, the specification provides several layout elements that can be used to arrange child elements with ease, such as stack or grid layout.

```xml
	<IMML Camera="Camera" xmlns="http://schemas.vastpark.com/2007/imml/">
		<Camera Name="Camera" Position="0,0,-5" />
		
		<Stack Spacing="0,1,0">
			<Model Source="robot-head.model" Size="1,1,1" />
			<Model Source="robot-body.model" Size="1,2,1" />
			<Model Source="robot-legs.model" Size="1,2,1" />
		</Stack>
	</IMML>
```
##### Compose from multiple independent documents

For increased flexibility, multiple IMML files can be combined to form a single scene via the Include element.

```xml
	<IMML Camera="Camera" xmlns="http://schemas.vastpark.com/2007/imml/">
		<Camera Name="Camera" Position="0,0,-5" />
		
		<Include Source="file1.imml" />
		<Include Source="file2.imml" />
		<Include Source="file3.imml" />
	</IMML>
```

#### License

IMML was developed at VastPark under the VastPark Open Specification Promise and has been made available on GitHub under the MIT license.
