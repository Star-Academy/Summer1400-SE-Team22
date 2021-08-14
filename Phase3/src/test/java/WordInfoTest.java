import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;

import static org.junit.jupiter.api.Assertions.*;

class WordInfoTest {

    WordInfo wordInfo;
    @BeforeEach
    void initialize() {
        wordInfo = new WordInfo("test", 20);
    }

    @Test
    void getFileNameTest() {
        assertEquals("test", wordInfo.getFileName());
    }

    @Test
    void getPositionTest() {
        assertEquals(20, wordInfo.getPosition());
    }
}