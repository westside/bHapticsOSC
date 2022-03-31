﻿using System;
using System.Threading;
using bHapticsOSC.Config;
using bHapticsOSC.Utils;
using bHapticsOSC.OpenSoundControl;

namespace bHapticsOSC
{
    internal static class Program
    {
        internal static int Main(string[] args)
        {
            bool isFirst;
            Mutex mutex = new Mutex(true, BuildInfo.Name, out isFirst);
            if (!isFirst)
                return 0;

            WelcomeMessage();

            ConfigManager.LoadAll();

            ConfigManager.Devices.OnFileModified += () =>
            {
                Console.WriteLine();
                Console.WriteLine("Devices.cfg Reloaded!");
                Console.WriteLine();
                PrintDevices();
            };

            ConfigManager.VRChat.OnFileModified += () =>
            {
                Console.WriteLine();
                Console.WriteLine("VRChat.cfg Reloaded!");
                Console.WriteLine();
                PrintVRChat();
            };

            PrintConnection();
            PrintDevices();
            PrintVRChat();

            bHaptics.Load();
            OscManager.Connect();

            Console.WriteLine();
            Console.WriteLine("Awaiting Packets...");
            Console.WriteLine("Press ESC to Exit...");
            Console.WriteLine();

            ConsoleKeyInfo keyInfo;
            while (((keyInfo = Console.ReadKey(true)) == null) || (keyInfo.Key != ConsoleKey.Escape))
                Thread.Sleep(ThreadedTask.UpdateRate);

            OnQuit();
            return 0;
        }

        private static void OnQuit()
        {
            OscManager.Disconnect();
            bHaptics.Quit();
            ConfigManager.SaveAll();
        }

        private static void WelcomeMessage()
        {
            Console.WriteLine(Console.Title = $"{BuildInfo.Name} v{BuildInfo.Version}");
            Console.WriteLine($"Created by Herp Derpinstine");
            Console.WriteLine();
            Console.WriteLine();
        }

        private static void PrintVRChat()
        {
            Console.WriteLine($"===== VRChat =====");
            Console.WriteLine();
            Console.WriteLine($"[InStation] = {ConfigManager.VRChat.vrchat.Value.InStation}");
            Console.WriteLine();
            Console.WriteLine();
        }

        private static void PrintConnection()
        {
            Console.WriteLine($"===== Receiver =====");
            Console.WriteLine();
            Console.WriteLine($"[Port] = {ConfigManager.Connection.receiver.Value.Port}");
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine($"===== Sender =====");
            Console.WriteLine();
            Console.WriteLine($"[Enabled] = {ConfigManager.Connection.sender.Value.Enabled}");
            Console.WriteLine($"[IP] = {ConfigManager.Connection.sender.Value.IP}");
            Console.WriteLine($"[Port] = {ConfigManager.Connection.sender.Value.Port}");
            Console.WriteLine();
            Console.WriteLine();
        }

        private static void PrintDevices()
        {
            Console.WriteLine($"===== Devices =====");
            Console.WriteLine();
            Console.WriteLine($"[Head] = {ConfigManager.Devices.Head.Value.Enabled}");
            Console.WriteLine($"[Vest] = {ConfigManager.Devices.Vest.Value.Enabled}");
            Console.WriteLine($"[Arm | Left] = {ConfigManager.Devices.ArmLeft.Value.Enabled}");
            Console.WriteLine($"[Arm | Right] = {ConfigManager.Devices.ArmRight.Value.Enabled}");
            Console.WriteLine($"[Hand | Left] = {ConfigManager.Devices.HandLeft.Value.Enabled}");
            Console.WriteLine($"[Hand | Right] = {ConfigManager.Devices.HandRight.Value.Enabled}");
            Console.WriteLine($"[Foot | Left] = {ConfigManager.Devices.FootLeft.Value.Enabled}");
            Console.WriteLine($"[Foot | Right] = {ConfigManager.Devices.FootRight.Value.Enabled}");
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine($"===== Intensity =====");
            Console.WriteLine();
            Console.WriteLine($"[Head] = {ConfigManager.Devices.Head.Value.Intensity}");
            Console.WriteLine($"[Vest] = {ConfigManager.Devices.Vest.Value.Intensity}");
            Console.WriteLine($"[Arm | Left] = {ConfigManager.Devices.ArmLeft.Value.Intensity}");
            Console.WriteLine($"[Arm | Right] = {ConfigManager.Devices.ArmRight.Value.Intensity}");
            Console.WriteLine($"[Hand | Left] = {ConfigManager.Devices.HandLeft.Value.Intensity}");
            Console.WriteLine($"[Hand | Right] = {ConfigManager.Devices.HandRight.Value.Intensity}");
            Console.WriteLine($"[Foot | Left] = {ConfigManager.Devices.FootLeft.Value.Intensity}");
            Console.WriteLine($"[Foot | Right] = {ConfigManager.Devices.FootRight.Value.Intensity}");
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}
