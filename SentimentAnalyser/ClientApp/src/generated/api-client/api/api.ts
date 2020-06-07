export * from './calculations.service';
import { CalculationsService } from './calculations.service';
export * from './oidcConfiguration.service';
import { OidcConfigurationService } from './oidcConfiguration.service';
export const APIS = [CalculationsService, OidcConfigurationService];
