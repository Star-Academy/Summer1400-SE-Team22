import java.io.File;
import java.io.FileWriter;
import java.io.IOException;

import static org.junit.jupiter.api.Assertions.*;

class FileReaderTest {

    @org.junit.jupiter.api.Test
    void readFileContent() {
        try {
            FileWriter fileWriter = new FileWriter("src/main/resources/temp.txt");
            fileWriter.write("hello world!");
            fileWriter.close();
        } catch (IOException e) {
            e.printStackTrace();
        }

        File tempFile = new File("src/main/resources/temp.txt");
        String content = FileReader.readFileContent(tempFile);

        assertEquals(content, "hello world!");

        tempFile.delete();
    }

    @org.junit.jupiter.api.Test
    void readNotExistingFileContent() {
        assertDoesNotThrow(() ->{
            FileReader.readFileContent(new File("src/main/resources/notExistingFile.xyz"));
        });
    }
}