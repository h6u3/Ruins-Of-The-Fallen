using NUnit.Framework;
using System.IO;

public class SavingMechanic
{
    private string testDirPath = "Assets/Scripts"; 
    private string testFileName = "test.json"; 

    [Test]
    public void TestLoad()
    {
        // Create a test file to load
        string fullPath = Path.Combine(testDirPath, testFileName);
        FileManager fileManager = new FileManager(testDirPath, testFileName);

        // Set the Health, Hydration, and Hunger properties in the test data
        File.WriteAllText(fullPath, "{ \"Health\": 1000, \"Hydration\": 50, \"Hunger\": 75 }"); 
        GameData loadedData = fileManager.Load();

        //Assertions (Examples using the GameData variables)
        Assert.IsNotNull(loadedData);
        Assert.AreEqual(1000, loadedData.Health); // Check the loaded Health property
        Assert.AreEqual(50, loadedData.Hydration); // Check the loaded Hydration property
        Assert.AreEqual(75, loadedData.Hunger); // Check the loaded Hunger property

        // Clean up after the test
        File.Delete(fullPath);
    }

    [Test]
    public void TestSave()
    {
        FileManager fileManager = new FileManager(testDirPath, testFileName);

        // Create a test GameData object to save
        GameData testData = new GameData();
        testData.Health = 123;

        fileManager.save(testData);

        // Check if the file was created
        string fullPath = Path.Combine(testDirPath, testFileName);
        Assert.IsTrue(File.Exists(fullPath));
     
        // Clean up after the test
        File.Delete(fullPath);
    }
}
