using System;

namespace PABXMonitor
{
    public static class SampleStarter
    {
        static SampleStarter()
        {
        }

        public static void StartSample()
        {
            try
            {
                Console.WriteLine("Starting.");

                Samples.DisplayAllSample.Run();

                Console.WriteLine("Done.");

            }
            catch (TCX.Configuration.Exceptions.DNNameIsNotSpecified e)
            {
                //will be thrown if name of DN is not specified while creating DN.
                Console.WriteLine("Exception: " + e);
            }
            catch (TCX.Configuration.Exceptions.ObjectAlreadyExistsException e)
            {
                //will be thrown if object already exists
                Console.WriteLine("Exception: " + e);
            }
            catch (TCX.Configuration.Exceptions.ObjectSavingException e)
            {
                //method Save() or Delete() can throw this exception in case if ObjectModel
                //can not save or delete object by some reason (constrait violation, or connection
                //to configuration server is not available)
                Console.WriteLine("Exception: " + e);
            }
            catch (TCX.Configuration.Exceptions.PhoneSystemException e)
            {
                //base exception for all 3CX phone system exceptions
                Console.WriteLine("Exception: " + e);
            }
            catch (Exception e)
            {
                //all other exceptions (runtime exceptions)
                Console.WriteLine("Exception: " + e);
            }
            finally
            {
                Console.WriteLine("Done.");
            }
        }
    }
}