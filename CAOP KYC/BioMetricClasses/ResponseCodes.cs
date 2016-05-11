using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BioMetricClasses
{
    public enum ResponseCodes
    {
        //Nadra
        successful = 100,
        citizen_number_is_not_verified = 110,
        finger_print_doesnot_exist_in_citizen_database = 111,
        error_generating_session_id = 112,
        invalid_verification_reference_number = 114,
        invalid_service_provide_transaction_id = 115,
        finger_verification_has_been_exhausted_for_current_finger = 118,
        verification_limit_for_current_citizen_number_has_been_exhausted = 119,
        invalid_input_finger_template = 120,
        invalid_finger_index = 121,
        finger_print_doesnot_match = 122,
        invalid_finger_template_type = 123,
        this_operation_will_only_be_enabled_if_biometric_verification_of_all_available_finger_is_failed = 124,
        contact_number_is_not_valid = 125,
        transaction_id_already_exist = 175,
        invalid_area_name = 185,
        invalid_account_type = 186,

        no_request_found_against_citizen_number_or_transaction_id = 151,
        last_verification_was_not_successful = 152,

        invalid_xml = 102,
        invalid_username_or_password = 103,
        invalid_session_id = 104,
        sesssion_has_been_expired = 201,
        verification_was_successful_therefore_session_has_been_expired = 202,
        user_do_not_have_access_to_this_functionality = 105,
        invalid_franchise_id = 106,
        invalid_citizen_number = 107,
        citizen_verification_service_is_down_Please_try_again_later = 108,
        Exception_system_has_encounter_an_unexpected_error_Administrator_has_been_informed_please_try_again_later = 109,
        daily_verification_limit_has_been_exhausted_for_testing = 180,

        //RBTS_NADRA Service Error
        CNIC_not_valid = 099,
        invalid_data = 098,
        invalidDevice = 097,
        invalidDate = 096,
        ErrorConnectingNadra = 095,
        Exception_system_has_encounter_an_unexpected_error = 094,

        //BioMetric Device

        Branch_Code_doesnot_exists = 056,
        Invalid_Input_parameters = 057,
        Device_Serial_doesn0t_exists = 058,
        Timed_out = 059,
        Device_is_Offline = 060,
        Invalid_Transaction_ID = 061,
        Operation_in_process = 062,

        //	PORTAL Connecting Device AND NADRA
        BioMetricDeviceNotConnecting = 301,
        SystemError = 302,
        BioMetricDeviceResponseIsInvalid = 303,
        NADRA_RBTS_Service_Not_Connecting = 304,
        InvalidDataAos = 305,
        AosDbError = 306,
        NoDeviceRegisteredInBranch = 308
    }
}
