﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using windows10_home_control;

namespace windows10_home_control.Migrations
{
    [DbContext(typeof(DeviceConfigDB))]
    [Migration("20160812205721_configDB3")]
    partial class configDB3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431");

            modelBuilder.Entity("windows10_home_control.Device", b =>
                {
                    b.Property<int>("DeviceId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DeviceAlarmState");

                    b.Property<bool>("DeviceEnable");

                    b.Property<string>("DeviceName");

                    b.Property<int>("DevicePin");

                    b.Property<int>("DeviceState");

                    b.Property<string>("DeviceType");

                    b.HasKey("DeviceId");

                    b.ToTable("Devices");
                });

            modelBuilder.Entity("windows10_home_control.rFidCards", b =>
                {
                    b.Property<int>("rFidCardsId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("rFidType");

                    b.Property<uint>("rFidUID");

                    b.HasKey("rFidCardsId");

                    b.ToTable("rFidCard");
                });
        }
    }
}
