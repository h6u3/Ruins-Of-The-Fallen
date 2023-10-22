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
        File.WriteAllText(fullPath, "{ \"Health\": 4939 }"); // Set the Health property in the test data
        File.WriteAllText(fullPath, "{ \"Hydration\": 234 }"); // Set the Hydration property in the test data
        File.WriteAllText(fullPath, "{ \"Hunger\": 10 }"); // Set the Hunger property in the test data
        FileManager fileManager = new FileManager(testDirPath, testFileName);
        GameData loadedData = fileManager.Load();

        //Assertions
        Assert.IsNotNull(loadedData);
        Assert.AreEqual(4939, loadedData.Health); 
        Assert.AreEqual(234, loadedData.Health); 
        Assert.AreEqual(10, loadedData.Health); 

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
