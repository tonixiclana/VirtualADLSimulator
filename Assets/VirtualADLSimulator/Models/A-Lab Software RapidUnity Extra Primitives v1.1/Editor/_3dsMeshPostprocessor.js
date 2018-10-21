// FbxMeshPostProcessor utility script
// Written by Roger Clark 20/04/2009
// 
// I wrote this script as the fbx models I was importing into Unity had the wrong "Scale factor"
// I also wanted all models to have a collider
// Note. When I did not set a value for generateMaterials, it appeared to default to not generating materials, 
// so I set generateMaterials to true.
class _3dsImporterMeshPostprocessor extends AssetPostprocessor 
{
	function OnPreprocessModel () 
	{
		var modelImporter : ModelImporter = assetImporter;
		var fname:String = modelImporter.assetPath.ToLower();
		Debug.Log(fname.Substring(fname.length - 4, 4));
		if (fname.Substring(fname.length - 4, 4)==".3ds")
		{
		
			//modelImporter.generateMaterials = 1;// Enable material creation
			//modelImporter.addCollider = true;// This tells it to add a collider to the object 
			modelImporter.globalScale=1;// This changes the "Scale Factor" to 1.0
		}
	}
}