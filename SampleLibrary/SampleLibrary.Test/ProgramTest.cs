using System;
using System.IO;
using Xunit;

namespace SampleLibrary.Test
{
    public class ProgramTest
    {
        [Fact]
        void mainTest()
        {
            // InputStream sysInBackup = System.in;
            // ByteArrayInputStream in = new ByteArrayInputStream(("hello\nexit").getBytes());
            // System.setIn(in);
            //
            // ByteArrayOutputStream outContent = new ByteArrayOutputStream();
            // System.setOut(new PrintStream(outContent));
            // outContent.reset();
            //
            // assertDoesNotThrow(() -> Main.main(null));
            //
            // assert(outContent.toString().contains("File name: "));
            //
            // System.setIn(sysInBackup);
        }

        [Fact]
        void mainTest2()
        {
            // InputStream sysInBackup = System.in;
            // ByteArrayInputStream in = new ByteArrayInputStream(("hello -kid\nexit").getBytes());
            // System.setIn(in);
            //
            // ByteArrayOutputStream outContent = new ByteArrayOutputStream();
            // System.setOut(new PrintStream(outContent));
            // outContent.reset();
            //
            // assertDoesNotThrow(() -> Main.main(null));
            //
            // assert(outContent.toString().contains("File name: "));
            //
            // System.setIn(sysInBackup);
        }
    }
}