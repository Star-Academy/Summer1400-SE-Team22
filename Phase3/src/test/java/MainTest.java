import org.junit.jupiter.api.Test;

import java.io.ByteArrayInputStream;
import java.io.ByteArrayOutputStream;
import java.io.InputStream;
import java.io.PrintStream;

import static org.junit.jupiter.api.Assertions.*;

class MainTest {

    @Test
    void mainTest() {
        InputStream sysInBackup = System.in;
        ByteArrayInputStream in = new ByteArrayInputStream(("hello\nexit").getBytes());
        System.setIn(in);

        ByteArrayOutputStream outContent = new ByteArrayOutputStream();
        System.setOut(new PrintStream(outContent));
        outContent.reset();

        assertDoesNotThrow(() -> Main.main(null));

        assert(outContent.toString().contains("File name: "));

        System.setIn(sysInBackup);
    }

    @Test
    void mainTest2() {
        InputStream sysInBackup = System.in;
        ByteArrayInputStream in = new ByteArrayInputStream(("hello -kid\nexit").getBytes());
        System.setIn(in);

        ByteArrayOutputStream outContent = new ByteArrayOutputStream();
        System.setOut(new PrintStream(outContent));
        outContent.reset();

        assertDoesNotThrow(() -> Main.main(null));

        assert(outContent.toString().contains("File name: "));

        System.setIn(sysInBackup);
    }
}