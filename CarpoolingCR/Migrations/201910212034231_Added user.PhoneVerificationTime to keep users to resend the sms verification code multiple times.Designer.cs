// <auto-generated />
namespace CarpoolingCR.Migrations
{
    using System.CodeDom.Compiler;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Infrastructure;
    using System.Resources;
    
    [GeneratedCode("EntityFramework.Migrations", "6.2.0-61023")]
    public sealed partial class AddeduserPhoneVerificationTimetokeepuserstoresendthesmsverificationcodemultipletimes : IMigrationMetadata
    {
        private readonly ResourceManager Resources = new ResourceManager(typeof(AddeduserPhoneVerificationTimetokeepuserstoresendthesmsverificationcodemultipletimes));
        
        string IMigrationMetadata.Id
        {
            get { return "201910212034231_Added user.PhoneVerificationTime to keep users to resend the sms verification code multiple times"; }
        }
        
        string IMigrationMetadata.Source
        {
            get { return null; }
        }
        
        string IMigrationMetadata.Target
        {
            get { return Resources.GetString("Target"); }
        }
    }
}