# AsepriteFile
The `AsepriteFile` class is what is generated from using the `AsepriteBinaryReader.Read()` method.  Logically, all types that belong to the `AsepriteFile` class such as `Frame`, `Layer`, `Slice`, etc. are only used as types within the `AsepriteFile` class.  Due to this, it was decided to make all types used within this class as nested classes of the `AsepriteFile` class.

To organize the code and not have one massive file, I decided to split them into their own files and have the `AsepriteFile` class as a partial class to house each one.
